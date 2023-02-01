<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="bg-dark text-light">
    <ModalComponent id="keepDetails">
      <KeepDetails :keep="activeKeep" />
    </ModalComponent>
    <!-- <ModalComponent id="createModal" /> -->
  </footer>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from './AppState';
import Navbar from './components/Navbar.vue';
import { keepsService } from "./services/KeepsService.js";
import { logger } from "./utils/Logger.js";
import Pop from "./utils/Pop.js";

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
      appState: computed(() => AppState),
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep)
    };
  },
  components: { Navbar }
};
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
