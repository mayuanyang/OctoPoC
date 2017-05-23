import { HttpClient } from 'aurelia-fetch-client';

export class AppSettingService {
    constructor() {
        this.webapiUrl = 'http://localhost:55696/api/OctopusManager';
        this.client = new HttpClient();
    }

    fetch() {
        var result = this.client.fetch(this.webapiUrl);
        return result;
    }
}