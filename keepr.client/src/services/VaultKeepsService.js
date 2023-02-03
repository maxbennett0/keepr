import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultKeepsService {
  async saveToVault(vkData) {
    const res = await api.post('api/vaultkeeps', vkData);
    logger.log(res.data);
    AppState.profileVaultKeeps.push(res.data);
  }
}

export const vaultKeepsService = new VaultKeepsService();