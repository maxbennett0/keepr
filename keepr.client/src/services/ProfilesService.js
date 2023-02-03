import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class ProfilesService {
  async getProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId);
    logger.log(res.data);
    AppState.profiles.push(res.data);
    AppState.activeProfile = res.data;
  }
  async getProfileVaults(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/vaults');
    logger.log("[GETTING PROFILE VAULTS]", res.data);
    AppState.profileVaults = res.data;
  }
  async getProfileKeeps(profileId) {
    logger.log(profileId);
    const res = await api.get('api/profiles/' + profileId + '/keeps');
    logger.log("[GETTING PROFILE KEEPS]", res.data);
    AppState.profileKeeps = res.data;
  }
}

export const profilesService = new ProfilesService();