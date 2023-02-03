<template>
  <div class="row">
    <div class="col-12">
      <div class="selectable" @click="setActive(vault.id)">
        <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id } }">
          <div v-if="vault.isPrivate == false || vault.creatorId == account.id">
            <img class="img-fluid rounded" :src="vault.img" alt="">
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
export default {
  props: {
    vault: {
      type: Object, required: true
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account),
      async setActive(vaultId) {
        try {
          await vaultsService.getVaultDetails(vaultId);
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

</style>