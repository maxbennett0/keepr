<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl ">
      <div class="modal-content text-dark">
        <form @submit.prevent="createVault">
          <div class="modal-body">
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.name" type="text" required class="form-control" id="name"
                placeholder="Vault Name">
              <label for="name">Vault Name</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.description" type="text" required class="form-control" id="description"
                placeholder="Description">
              <label for="description">Description</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.img" type="url" required class="form-control" id="img" placeholder="Vault Image">
              <label for="coverImg">Image</label>
            </div>
            <div class="d-flex align-items-center text-center">
              <input v-model="editable.isPrivate" type="checkbox" id="isPrivate" placeholder="Private Vault?">
              <h6 class="m-2 d-flex align-items-center">Private Vault?</h6>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Create Vault</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async createVault() {
        try {
          const vault = await vaultsService.createVault(editable.value);
          editable.value = {};
          Modal.getOrCreateInstance('#createVault').hide();
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