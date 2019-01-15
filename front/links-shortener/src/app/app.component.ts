import { Component, OnInit } from '@angular/core';
import { AppState } from './app-state';
import { FormGroup, FormControl} from '@angular/forms';
import { BackendService} from './backend-service.service';
import { LinkModel } from './link-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'Skillaz test by Ivan Trofimov';

  appState = AppState.initial;
  isChangingLogin: boolean = false;

  login: string;
  
  destinationUrl: string;
  shortenedUrl: string;

  myLinks: LinkModel[] = [];

  redirectPrefix: string = 'r/';

  constructor(private backendService: BackendService){}

  ngOnInit() {
    this.loadLogin();
    this.getMyLinks();
  }

  isInitialHidden() {
    return this.appState != AppState.initial;
  }

  isErrorHidden() {
    return this.appState != AppState.error;
  }

  isSuccessHidden() {
    return this.appState != AppState.success;
  }

  goToInitial() {
    this.destinationUrl = null;
    this.shortenedUrl = null;
    this.appState = AppState.initial;
  }

  onLinksFormSubmit() {
    this.backendService.createLink(this.destinationUrl, this.transformLogin(this.login)).subscribe(
      result => {
        this.appState = AppState.success;
        this.shortenedUrl = result.url;
        this.myLinks.push(result);
      },
      error => {
        this.appState = AppState.error;
      }
    );
  }

  getMyLinks() {
    this.backendService.getMyLinks(this.transformLogin(this.login)).subscribe(
      result => {
        this.myLinks = result;
      }
    );
  }

  parseDate(dateStr: string) : string {
    const months = ['янв', 'фев', 'марта', 'апр', 'мая', 'июня', 'июля', 
      'авг', 'сен', 'окт', 'ноя', 'дек'];
      
    let date = new Date(dateStr);
    let mins = date.getMinutes();

    return date.getDay().toString() + ' ' + months[date.getMonth()] + ', '
      + date.getHours() + ':' + 
      (mins > 10 ? mins : ('0' + mins.toString()));
  }

  transformLogin(login: string) : string {
    return 'l_' + login;
  }

  buildLink(url: string) : string {
    return window.location.href + this.redirectPrefix + url;
  }

  copyShortenedToClipboard() {
    this.copyToClipboard(this.buildLink(this.shortenedUrl));
  }

  copyToClipboard(value: string) {
    let selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = value;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
  }

  newLogin: string;

  setLoginChanging() {
    this.newLogin = null;
    this.isChangingLogin = true;
  }

  unsetLoginChanging() {
    this.isChangingLogin = false;
  }

  loginKey: string = 'skillaz_login_key';

  loadLogin() {
    let storageValue = localStorage.getItem(this.loginKey);

    this.login = storageValue == null ? 'Vadim Zhogalski' : storageValue;
  }

  getMyLinksSorted() {
    return this.myLinks.sort((v1, v2) => {
      if (v1.createdAt < v2.createdAt) return 1;
      if (v1.createdAt > v2.createdAt) return -1;
      return 0;
    });
  }

  setLogin() {  
    localStorage.setItem(this.loginKey, this.newLogin);

    this.login = this.newLogin;

    this.unsetLoginChanging();
    this.getMyLinks();
  }
}
