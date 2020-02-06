import { Component, OnInit, HostBinding } from '@angular/core';
import { AdminStateFacade } from '../+state/facades/admin.facade';

@Component({
  selector: 'scx-header',
  template: `
    <button class="navbar-toggler" type="button" scxMobileSidebarToggler></button>
    <a class="navbar-brand" [class]="{'navbar-brand-hidden': isHidden}" href="#">
      <span class=" raised-button small-logo text-primary">CS</span>
      <span *ngIf="isMaximized" class="logo">Spotacard</span>
    </a>
    <button class="navbar-toggler" type="button" scxSidebarToggler></button>
    <ng-content></ng-content>
    <button class="navbar-toggler" type="button" scxAsideToggler></button>
  `,
  styles: [`
    .navbar-brand-hidden {
      box-shadow: none;
      width: fit-content !important;
    }
  `]
})
export class HeaderComponent implements OnInit {
  @HostBinding('class.app-header') class1 = true;
  @HostBinding('class.navbar') class2 = true;
  public isMaximized = false;
  public isHidden = false;
  constructor(
    private facade: AdminStateFacade
  ) {}

  ngOnInit() {
    this.facade.sidebar$.subscribe(sidebar => {
      if(sidebar.state === 'MAXIMIZED'){
        this.isMaximized = true;
      } else {
        this.isMaximized = false;
      }

      if(sidebar.state === 'HIDDEN'){
        this.isHidden = true;
      } else {
        this.isHidden = false;
      }
    })
  }
}
