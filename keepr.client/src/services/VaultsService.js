import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultsService {
  async getVault() {
    const res = api.get('api/vaults');
    logger.log('[GETTING VAULTS]', res.data);
    AppState.vaults = res.data;
  }
}

export const vaultsService = new VaultsService();