<template>
  <div v-if="account" class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl ">
      <div class="modal-content text-dark">
        <form @submit.prevent="editAccount(account.id)">
          <div class="modal-body">
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.name" type="text" required class="form-control" id="name"
                placeholder="New Account Name">
              <label for="name">Account Name</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.picture" type="url" required class="form-control" id="img"
                placeholder="Vault Image">
              <label for="coverImg">Account Picture</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
              <input v-model="editable.coverImg" type="url" required class="form-control" id="img"
                placeholder="Vault Image">
              <label for="coverImg">Account Cover Image</label>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Edit Account</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      account: computed(() => AppState.account),
      async editAccount(accountId) {
        try {
          let accountData = editable.value;
          await accountService.editAccount(accountId, accountData);
          Modal.getOrCreateInstance('#editAccount').hide();
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