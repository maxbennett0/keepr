<template>
  <div v-if="keep" class="card text-white border-0 rounded-4 elevation-5 m-3 selectable hover" :title="keep.name"
    @click="setActive(keep.id), getAccountVaults()" data-bs-toggle="modal" data-bs-target="#keepDetails">
    <img :src="keep.img" class="card-img rounded-4" alt="...">
    <div class="card-img-overlay d-flex align-items-end justify-content-between">
      <h5 class="card-title text-shadow mx-2 fw-bold">{{ keep.name }}</h5>
      <img :src="keep.creator.picture" height="40" width="40" class="person rounded-circle elevation-2" alt=""
        :title="keep.creator.name">
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { profilesService } from "../services/ProfilesService.js";
import { accountService } from "../services/AccountService.js";
export default {
  props: {
    keep: { type: Object, required: true }
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
      async getProfileVaults(profileId) {
        try {
          await profilesService.getProfileVaults(profileId);
        } catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },
      async getAccountVaults() {
        try {
          await accountService.getMyVaults();
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
.card {
  position: relative;
}

@media screen AND (max-width: 768px) {
  .person {
    display: none;
  }

  .card-title {
    display: none;
  }
}
</style>