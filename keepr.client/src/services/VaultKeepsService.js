import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultKeepsService {
  async saveToVault(vkData) {
    const res = await api.post('api/vaultkeeps', vkData);
    logger.log(res.data);
    AppState.profileVaultKeeps.push(res.data);
  }

  async deleteVaultKeep(keepId) {
    const res = await api.delete('api/vaultkeeps/' + keepId);
    logger.log(res.data);
    AppState.vaultKeeps = AppState.vaultKeeps.filter(v => v.keepId != keepId);
  }
}

export const vaultKeepsService = new VaultKeepsService();