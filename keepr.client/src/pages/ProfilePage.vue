<template>
  <h1>{{ activeProfile }}</h1>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import { useRoute } from "vue-router";
export default {
  setup() {
    const route = useRoute();
    async function getProfile() {
      try {
        await profilesService.getProfile(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getProfileVaults() {
      try {
        await profilesService.getProfileVaults(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.profileId);
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getProfile();
      getProfileVaults();
      getProfileKeeps();
    });
    return {
      activeProfile: computed(() => AppState.activeProfile)
    };
  }
};
</script>


<style lang="scss" scoped>

</style>