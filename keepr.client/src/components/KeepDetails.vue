<template>
  <div v-if="keep" class="row justify-content-evenly align-content-center">
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
          <div class="mx-2">
            <select name="vaults" id="vaults">
              <option value="yo mama">yo mama</option>
              <option value="yo mama">yo mama</option>
              <option value="yo mama">yo mama</option>
              <option value="yo mama">yo mama</option>
            </select>
          </div>
        </div>
        <div class="col-6 d-flex justify-content-end">
          <div>
            <router-link :to="{ name: 'ProfilePage', params: { profileId: keep.creatorId } }">
              <img class="img-modal rounded-circle img-fluid mx-2" :src="keep.creator.picture" alt="">
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
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
export default {
  props: {
    keep: {
      type: Object, required: true
    }
  },
  setup() {
    return {
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