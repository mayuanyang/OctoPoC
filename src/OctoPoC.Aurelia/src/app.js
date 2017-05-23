export class App {
  configureRouter(config, router) {
    this.router = router;
    config.title = 'Aurelia';
    config.map([
      { route: ['', 'home'],   nav: true,     name: 'home',  title: 'Home',      moduleId: 'home/home' },
      { route: ['settings'],    nav: true,   name: 'settings', title: 'Settings',       moduleId: 'appsettings/appsettings' },
      { route: ['http'],    nav: true,   name: 'http', title: 'YouTube',       moduleId: 'httppratice/http-pratice' },
      { route: ['validation'],    nav: true,   name: 'validation', title: 'Validation Pratice',       moduleId: 'validation/validation' },
      { route: ['dynamicloader'],    nav: true,   name: 'dynamicloader', title: 'Dynamic UI Composition',       moduleId: 'dynamicloader/dynamic-loader' },
      { route: ['nosql'],    nav: true,   name: 'nosql', title: 'Connect to Database',       moduleId: 'nosql/nosql' },
      { route: ['authentication'],    nav: true,   name: 'authentication', title: 'Authentication',       moduleId: 'authentication/authentication' },
      { route: ['about'],      nav: true,   name: 'about me', title: 'About Me',      moduleId: 'aboutme/aboutme' },
      { route: ['contact'],    nav: true,   name: 'contact me', title: 'Contact Me',       moduleId: 'contactme/contactme' },
    ]);
  }

  constructor() {
    this.message = 'Hello World!';
  }
}
