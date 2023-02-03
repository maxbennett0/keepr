<template>
  <div class="row">
    <div class="col-12 text-center ">
      <div class="selectable text-center" @click="setActive(keep.id)" data-bs-toggle="modal"
        data-bs-target="#keepDetails">
        <img class="img-fluid rounded mb-2" :src="keep.img" alt="">
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
export default {
  props: {
    keep: {
      type: Object, required: true
    }
  },
  setup() {
    return {
      async setActive(keepId) {
        try {
          await keepsService.getKeepDetails(keepId);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
    };
  }
};
</script>


<style lang="scss" scoped>

</style>