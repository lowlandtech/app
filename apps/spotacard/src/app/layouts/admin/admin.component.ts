import { Component, OnInit } from '@angular/core';
import { dark, darker, Sheet } from '@spotacard/theme';
import jss from 'jss';
import { User } from '@spotacard/api';
import { Observable } from 'rxjs';
import { AuthFacade, LocalStorageJwtService } from '@spotacard/auth';
import { take, filter } from 'rxjs/operators';
import { AdminStateFacade, AsideListItemComponent } from '@spotacard/admin';

const styles = {
  footer: {
    borderTop: `1px solid ${dark}`,
    background: dark,
    boxShadow: '0 -5px 5px -5px rgba(0, 0, 0, .2)',
    color: darker,
  },
  container: {
    padding: '10px'
  }
};

@Component({
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  public user$: Observable<User>;
  public isLoggedIn$: Observable<boolean>;
  public classes: any;

  constructor(
    private authFacade: AuthFacade,
    private adminFacade: AdminStateFacade,
    private localStorageJwtService: LocalStorageJwtService) { }

  public ngOnInit(): void {
    const sheet: Sheet = jss.createStyleSheet(styles, { link: true }).attach();
    this.classes = sheet.classes;
    this.user$ = this.authFacade.user$;
    this.isLoggedIn$ = this.authFacade.isLoggedIn$;
    this.localStorageJwtService
      .getItem()
      .pipe(
        take(1),
        filter(token => !!token),
      )
      .subscribe(() => this.authFacade.user());
  }

  public addPanel(){
    this.adminFacade.asideListItemAdd({
      title: '(new panel)',
      component: AsideListItemComponent
    });
  }
}
