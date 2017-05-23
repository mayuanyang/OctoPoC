import {AppSettingService} from '../modules/appsetting.service';
import {inject} from 'aurelia-framework';

@inject(AppSettingService)
export class Appsettings {     
  constructor(appSettingService) {
    this.appsettingService = appSettingService;
  }

  getSettings(){
    console.log('sssss');
    var self = this;
    this.appsettingService.fetch()
      .then(response => response.json())
      .then(data => {
                self.settings = data;
                console.log(self.settings);
            });;
  }
}