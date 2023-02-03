<template>
  <div class="container-fluid" v-if="activeProfile">
    <div class="row">
      <div class="col-12 d-flex justify-content-center">
        <div class="p-5">
          <div>
            <img class=" cover-img elevation-3" :src="activeProfile.coverImg" alt="">
          </div>
          <div class="d-flex justify-content-center my-2">
            <img class="img-fluid rounded-circle profileImg elevation-3" :src="activeProfile.picture" alt="">
          </div>
          <h1 class="text-center my-5">{{ activeProfile.name }}</h1>
          <h4 class="text-center">{{ profileVaults.length }} Vaults | {{ profileKeeps.length }} Keeps</h4>
        </div>
      </div>
      <h1 class="my-4">Vaults:</h1>
      <div v-if="profileVaults" class="col-12 d-flex flex-wrap">
        <div v-for="v in profileVaults" class="row mx-2 ">
          <div class="col-12 d-flex flex-wrap">
            <VaultCard :vault="v" />
          </div>
        </div>
      </div>
      <h1 class="my-4">Keeps:</h1>
      <div class="col-12 d-flex">
        <div v-if="profileKeeps" class="row mx-2 flex-wrap">
          <div v-for="k in profileKeeps" class="col-4">
            <ProfKeeps :keep="k" />
          </div>
        </div>
      </div>
    </div>
  </div>
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
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults)
    };
  }
};
</script>


<style lang="scss" scoped>
.cover-img {
  height: 30vh;
  width: 100vh;
  object-fit: cover;
}

.profileImg {
  padding: 0%;
  margin: 0%;
  object-fit: cover;
  position: absolute;
  top: 265px;
  height: 15vh;
  width: 15vh;
}
</style>