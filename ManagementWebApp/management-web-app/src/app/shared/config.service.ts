import { Injectable } from '@angular/core';
 
@Injectable()
export class ConfigService {    

    constructor() {}

    get authApiURI() {
        return 'http://localhost:32772/api';
    }    
     
    get resourceApiURI() {
        return 'http://localhost:80/api';
    }  
}
