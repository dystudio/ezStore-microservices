// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  identityServiceUrl: 'http://localhost:55161/',
  filesystemServiceUrl: 'http://localhost:55159/api/',
  productServiceUrl: 'http://localhost:45162/api/',
  warehouseServiceUrl: 'http://localhost:45163/api/',
};
