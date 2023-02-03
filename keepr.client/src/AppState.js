import { reactive } from 'vue';

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  profiles: [],
  activeProfile: null,

  keeps: [],
  profileKeeps: [],
  accountKeeps: [],
  vaults: [],
  profileVaults: [],
  accountVaults: [],
  vaultKeeps: [],
  profileVaultKeeps: [],
  activeKeep: null,
  activeVault: null,
  activeVaultKeep: null
});
