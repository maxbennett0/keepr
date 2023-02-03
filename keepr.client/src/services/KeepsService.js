import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps');
    logger.log("[GETTING KEEPS]", res.data);
    AppState.keeps = res.data;
  }

  async getKeepDetails(keepId) {
    const res = await api.get('api/keeps/' + keepId);
    logger.log("[GETTING ACTIVE KEEP]", res.data);
    AppState.activeKeep = res.data;
    if (AppState.activeKeep.creatorId != AppState.account.id) {
      AppState.activeKeep.views++;
    }
  }
  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData);
    logger.log('[CREATING KEEP]', keepData);
    AppState.keeps.push(res.data);
  }
  async deleteKeep(keepId) {
    const res = await api.delete('api/keeps/' + keepId);
    logger.log('[DELETING KEEP]', res.data);
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId);
  }
}

export const keepsService = new KeepsService();