import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {LinkModel} from './link-model';

@Injectable({
    providedIn: 'root'
})
export class BackendService {
    private apiPrefix = 'http://localhost:34131/api/links';

    private linksCreationUrl = this.apiPrefix;
    private linksRetrievalUrl = this.apiPrefix + '/owner/';

    private headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});

    constructor(private http: HttpClient){}

    createLink(destination: string, ownerId: string) : Observable<LinkModel> {
        let requestData = {
            'Destination': destination,
            'ownerId': ownerId
        };

        return this.http.post<LinkModel>(this.linksCreationUrl, JSON.stringify(requestData),
            {headers: this.headers});
    }

    getMyLinks(ownerId: string) : Observable<LinkModel[]> {
        return this.http.get<LinkModel[]>(this.linksRetrievalUrl + ownerId);
    }
}