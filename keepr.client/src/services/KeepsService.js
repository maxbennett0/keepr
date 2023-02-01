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
}

export const keepsService = new KeepsService();