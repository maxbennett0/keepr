<template>
  <div v-if="vault" class="container - fluid">
    <div class="row">
      <div class="col-12 d-flex justify-content-center">
        <div
          class="d-flex flex-column justify-content-end align-items-center img-fluid vaultImg rounded my-3 elevation-2"
          :style="`background-image: url(${vault.img})`">
          <h1 class="text-center d-flex my-4 fw-bold text-white text-shadow fs-1">{{ vault.name }}
          </h1>
          <h6 class="text-center d-flex my-3 fw-bold text-white text-shadow fs-1">By: {{ vault.creator.name }}</h6>
          <h1 title="Delete Vault" v-if="account.id == vault.creatorId"><i
              class="mdi mdi-delete selectable text-danger text-shadow" @click="deleteVault(vault.id)"></i></h1>
        </div>
      </div>
      <h1 class="text-center">{{ profileVaultKeeps.length }} Keeps</h1>
    </div>
    <div class="row">
      <div v-for="v in profileVaultKeeps" class="col-2 d-flex justify-content-center vaultKeep">
        <div v-if="account.id == vault.creatorId" class="d-flex align-items-center">
          <i class="mdi mdi-delete fs-3 selectable" title="Delete Vault Keep" @click="deleteVK(v.vaultKeepId)"></i>
        </div>
        <KeepCard :keep="v" />
      </div>
    </div>
  </div>

  <div class="row" v-if="check == true">
    <div class="col-12 justify-content-center align-items-center d-flex">
      <h1 class="text-center">...this is not the vault you are looking for...</h1>
    </div>
  </div>
</template>
  

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { useRoute, useRouter } from "vue-router";
export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const check = ref(false);

    async function getVaultById() {
      try {
        await vaultsService.getVaultDetails(route.params.vaultId);
      } catch (error) {
        check.value = true;
        setTimeout(() => {
          router.push({ name: 'Home' });
        }, 3000);
      }
    }
    async function getVaultKeeps() {
      try {
        await vaultsService.getVaultKeeps(route.params.vaultId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getVaultById();
      getVaultKeeps();
    });
    return {
      check,
      vault: computed(() => AppState.activeVault),
      profileVaultKeeps: computed(() => AppState.profileVaultKeeps),
      account: computed(() => AppState.account),
      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm("ayo mista white.. you really wanna delete this vault keep?", "this is insane yo")) {
            await vaultsService.deleteVault(vaultId);
          }
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
      async deleteVK(vkId) {
        try {
          if (await Pop.confirm("ayo mista white.. you really wanna delete this vault keep?", "this is insane yo")) {
            await vaultKeepsService.deleteVaultKeep(vkId);
          }
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
.vaultImg {
  height: 35vh;
  width: 70vh;
  object-fit: cover;
  background-position: center;
}

.vaultKeep {
  object-fit: cover;
}

.text-shadow {
  text-shadow: 2px 2px black;
}
</style>