import { Component, OnInit, Input } from '@angular/core';
import { dark, darker, boxShadow, Sheet } from '@spotacard/theme';
import jss from 'jss';
import { User } from '@spotacard/api';
import { Observable } from 'rxjs';
import { AuthFacade, LocalStorageJwtService } from '@spotacard/auth';
import { take, filter } from 'rxjs/operators';

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
  template: `
    <scx-header>
      <ul *ngIf="!isLoggedIn$|async" class="nav navbar-nav pull-xs-right">
        <li class="nav-item">
          <!-- Add "active" class when you're on that page" -->
          <a class="nav-link" routerLink="/">Home</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" routerLink="/login" routerLinkActive="active"
            >Sign in</a
          >
        </li>
        <li class="nav-item">
          <a class="nav-link" routerLink="/register" routerLinkActive="active"
            >Sign up</a
          >
        </li>
      </ul>

      <!-- Logged in user -->
      <ul *ngIf="isLoggedIn$|async" class="nav navbar-nav pull-xs-right">
        <li class="nav-item">
          <!-- Add "active" class when you're on that page" -->
          <a
            class="nav-link active"
            [routerLink]="['/']"
            routerLinkActive="active"
            [routerLinkActiveOptions]="{ exact: true }"
            >Home</a
          >
        </li>
        <li class="nav-item">
          <a class="nav-link" routerLink="/editor" routerLinkActive="active">
            <i class="ion-compose"></i>&nbsp;New Card
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link" routerLink="/settings" routerLinkActive="active">
            <i class="ion-gear-a"></i>&nbsp;Settings
          </a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            [routerLink]="['/profile', (user$|async).username]"
            routerLinkActive="active"
          >
            <img [src]="(user$|async).image" *ngIf="(user$|async).image" class="user-pic" />
            {{ (user$|async).username }}
          </a>
        </li>
      </ul>
    </scx-header>
    <scx-content>
      <scx-sidebar>
        <scx-sidebar-header></scx-sidebar-header>
        <scx-sidebar-form></scx-sidebar-form>
        <scx-sidebar-nav></scx-sidebar-nav>
        <scx-sidebar-footer></scx-sidebar-footer>
        <button
          class="sidebar-minimizer"
          type="button"
          scxSidebarMinimizer
          scxBrandMinimizer
        ></button>
      </scx-sidebar>
      <scx-main [class]="classes.container">
        <scx-breadcrumbs></scx-breadcrumbs>
        <router-outlet></router-outlet>
      </scx-main>
      <scx-aside>
        <scx-aside-list></scx-aside-list>
      </scx-aside>
    </scx-content>
    <scx-footer [class]="classes.footer">
      <span><a href="https://spotacard.com">Spotacard</a> &copy; 2020</span>
      <span class="ml-auto"
        >Powered by <a href="https://lowlandtech.com">LowLandTech</a></span
      >
    </scx-footer>
  `
})
export class AdminComponent implements OnInit {
  public user$: Observable<User>;
  public isLoggedIn$: Observable<boolean>;
  public classes: any;

  constructor(
    private authFacade: AuthFacade,
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
      .subscribe(token => this.authFacade.user());

  }
}
