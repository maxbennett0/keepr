<template>
  <div class="container-fluid">
    <div class="masonry my-5">
      <div v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  }
};
</script>

<style scoped lang="scss">
.masonry {
  columns: 4;
}

@media screen AND (max-width: 768px) {
  .masonry {
    columns: 2;
  }
}
</style>
