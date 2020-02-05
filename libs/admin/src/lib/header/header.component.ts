import { Component, OnInit, HostBinding } from '@angular/core';
import { AdminStateFacade } from '../+state/facades/admin.facade';

@Component({
  selector: 'scx-header',
  template: `
    <button class="navbar-toggler" type="button" scxMobileSidebarToggler></button>
    <a class="navbar-brand" href="#">
      <span class=" raised-button small-logo text-primary">CS</span>
      <span *ngIf="showLogo" class="logo">Spotacard</span>
    </a>
    <button class="navbar-toggler" type="button" scxSidebarToggler></button>
    <ng-content></ng-content>
    <button class="navbar-toggler" type="button" scxAsideToggler></button>
  `
})
export class HeaderComponent implements OnInit {
  @HostBinding('class.app-header') class1 = true;
  @HostBinding('class.navbar') class2 = true;
  public showLogo = false;
  constructor(
    private facade: AdminStateFacade
  ) {}

  ngOnInit() {
    this.facade.sidebar$.subscribe(sidebar => {
      if(sidebar.state === 'MAXIMIZED'){
        this.showLogo = true;
      } else {
        this.showLogo = false;
      }
    })
  }
}
