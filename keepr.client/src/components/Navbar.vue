<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-secondary px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <button class="btn btn-success">Home</button>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <button v-if="account.id != null" class="btn btn-secondary mx-2" data-bs-toggle="modal"
          data-bs-target="#createModal">Create
          Keep</button>
        <button v-if="account.id != null" class="btn btn-secondary mx-2" data-bs-toggle="modal"
          data-bs-target="#createVault">Create
          Vault</button>
        <button v-if="account.id != null" class="btn btn-secondary" data-bs-toggle="modal"
          data-bs-target="#editAccount">Edit
          Account</button>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { AppState } from "../AppState.js";
import Login from './Login.vue';
import { computed } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
    };
  },
  components: { Login }
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
