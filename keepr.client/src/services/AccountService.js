import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import { api } from './AxiosService';

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account');
      AppState.account = res.data;
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err);
    }
  }
  async editAccount(accountId, accountData) {
    try {
      const res = await api.put('account/' + accountId, accountData);
      logger.log('[EDITING ACCOUNT]', res.data);
      AppState.account = res.data;
    } catch (error) {
      logger.error(error);
    }
  }

  async getMyVaults() {
    const res = await api.get('account/vaults');
    logger.log('[GETTING ACCOUNT VAULTS]', res.data);
    AppState.accountVaults = res.data;
  }
}

export const accountService = new AccountService();
