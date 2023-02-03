<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl ">
      <div class="modal-content text-dark">
        <form @submit.prevent="createKeep">
          <div class="modal-body">
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.name" type="text" required class="form-control" id="name"
                placeholder="Keep Name">
              <label for="name">Keep Name</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.description" type="text" required class="form-control" id="description"
                placeholder="Description">
              <label for="description">Description</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.img" type="url" required class="form-control" id="img" placeholder="Keep Image">
              <label for="coverImg">Image</label>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Create Keep</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";
export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async createKeep() {
        const keep = await keepsService.createKeep(editable.value);
        editable.value = {};
        Modal.getOrCreateInstance('#createModal').hide();
      }
    };
  }
};
</script>


<style lang="scss" scoped>

</style>