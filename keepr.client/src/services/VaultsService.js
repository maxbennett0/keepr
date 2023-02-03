import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultsService {
  async getVault() {
    const res = await api.get('api/vaults');
    logger.log('[GETTING VAULTS]', res.data);
    AppState.vaults = res.data;
  }

  async getVaultDetails(vaultId) {
    const res = await api.get('api/vaults/' + vaultId);
    logger.log('[GETTING ACTIVE VAULT]', res.data);
    AppState.activeVault = res.data;
  }

  async getVaultKeeps(vaultId) {
    const res = await api.get('api/vaults/' + vaultId + '/keeps');
    logger.log('[GETTING VAULT KEEPS]', res.data);
    AppState.profileVaultKeeps = res.data;
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData);
    logger.log('[CREATING VAULT]', res.data);
    AppState.vaults.push(res.data);
  }
  async deleteVault(vaultId) {
    const res = await api.delete('api/vaults/' + vaultId);
    logger.log('[DELETING VAULT]', res.data);
    AppState.vaults = AppState.vaults.filter(v => v.id != vaultId);
  }
}

export const vaultsService = new VaultsService();