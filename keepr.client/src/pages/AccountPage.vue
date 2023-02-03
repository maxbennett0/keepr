<template>
  <div class="container-fluid" v-if="account">
    <div class="row">
      <div class="col-12 d-flex justify-content-center">
        <div class="p-5">
          <div>
            <img class="rounded cover-img elevation-3" :src="account.coverImg" alt="">
          </div>
          <div class="d-flex justify-content-center my-2">
            <img class="img-fluid rounded-circle profileImg elevation-3" :src="account.picture" alt="">
          </div>
          <h1 class="text-center my-5">{{ account.name }}</h1>
          <h4 class="text-center">{{ accountVaults.length }} Vaults | {{
            profileKeeps.length
          }} Keeps
          </h4>
        </div>
      </div>
      <h1 class="my-4">Vaults:</h1>
      <div class="col-12 d-flex">
        <div v-for="v in accountVaults" class="d-flex mx-2">
          <VaultCard :vault="v" />
        </div>
      </div>
      <h1 class="my-4">Keeps:</h1>
      <div class="col-12 d-flex">
        <div v-for="k in profileKeeps" class="d-flex mx-2">
          <ProfKeeps :keep="k" />
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
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    const route = useRoute();
    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getMyVaults();
    });
    return {
      activeProfile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      accountVaults: computed(() => AppState.accountVaults),
      profileKeeps: computed(() => AppState.profileKeeps),
      async getProfileKeeps(accountId) {
        try {
          await profilesService.getProfileKeeps(accountId);
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