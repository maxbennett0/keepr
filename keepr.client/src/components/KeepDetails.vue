<template>
  <div v-if="keep && accountVaults" class="row justify-content-evenly align-content-center">
    <div class="col-6 pm-0">
      <img class="img-fluid img-card rounded" :src="keep.img" alt="">
    </div>
    <div class="col-6 d-flex flex-column justify-content-evenly">
      <div class="row justify-content-evenly">
        <div class="d-flex justify-content-center">
          <i class="mdi mdi-eye mx-2"></i>
          <p>{{ keep.views }}</p>
          <i class="mdi mdi-alpha-k-box-outline mx-2"></i>
          <p>{{ keep.kept }}</p>
        </div>
      </div>
      <div class="row">
        <div>
          <h1 class="text-center">{{ keep.name }}</h1>
          <h6 class="text-center text-secondary mx-2">{{ keep.description }}</h6>
        </div>
      </div>
      <div class="row">
        <div class="col-6 d-flex justify-content-start align-items-center">
          <div class="mx-5">
            <select v-model="editable.id" v-if="account" name="vaults" id="vaults">
              <option v-for="v in accountVaults" :value="v.id">{{ v.name }}</option>
            </select>
            <button class="btn btn-primary" @click="saveToVault(editable, keep.id)">Save</button>
          </div>
        </div>
        <div class="col-6 d-flex justify-content-end">
          <div v-if="keep.creatorId == account.id" class="d-flex fs-4 mx-2 selectable" @click="deleteKeep(keep.id)">
            <i class="mdi mdi-delete d-flex align-items-center"></i>
            <h6 class="d-flex align-items-center text-center m-0">Delete</h6>
          </div>
          <div>
            <router-link :to="{ name: 'ProfilePage', params: { profileId: keep.creatorId } }">
              <img class="img-modal rounded-circle img-fluid mx-2" :src="keep.creator.picture" alt=""
                data-bs-dismiss="modal">
            </router-link>
          </div>
          <div class="d-flex align-items-center">
            <h6 class="mx-2">{{ keep.creator.name }}</h6>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";
import { profilesService } from "../services/ProfilesService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { accountService } from "../services/AccountService.js";
export default {
  props: {
    keep: {
      type: Object, required: true
    }
  },
  setup() {
    const editable = ref({});
    return {
      editable,
      account: computed(() => AppState.account),
      profileVaults: computed(() => AppState.profileVaults),
      activeVault: computed(() => AppState.activeVault),
      accountVaults: computed(() => AppState.accountVaults),
      async deleteKeep(keepId) {
        try {
          await keepsService.deleteKeep(keepId);
          Modal.getOrCreateInstance('#keepDetails').hide();
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
      async saveToVault(editable, keepId) {
        try {
          let vkData = {
            vaultId: editable.id,
            keepId: keepId
          };
          await vaultKeepsService.saveToVault(vkData);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      }
    };
  }
};
</script>


<style lang="scss" scoped>
.img-card {
  width: 100%;
}

.pm-0 {
  padding: 0%;
  margin: 0%;
}

.img-modal {
  width: 7vh;
}
</style>