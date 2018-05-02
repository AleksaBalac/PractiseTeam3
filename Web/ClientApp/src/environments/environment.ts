// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

let apiUrl: string;

if (document.getElementsByTagName('base')[0].href === 'http://localhost:5000/') {
  apiUrl = 'http://localhost:5001/api';
} else {
  apiUrl = 'http://188.2.214.126:85/api';
}

export const environment = {
  production: false,
  webAddress: 'http://localhost:5000',
  apiAddress: apiUrl,
};
