import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-header',
  template: `
    <button class="navbar-toggler d-lg-none" type="button" scxMobileSidebarToggler>
      <span class="navbar-toggler-icon"></span>
    </button>
    <a class="navbar-brand" href="#">
    <span class=" raised-button small-logo text-primary">CS</span>
    <span class="logo">Spotacard</span>
  </a>
    <button class="navbar-toggler d-md-down-none mr-auto" type="button" scxSidebarToggler>
      <span class="navbar-toggler-icon"></span>
    </button>
    <ng-content></ng-content>
    <button
      class="navbar-toggler d-md-down-none"
      type="button"
      scxAsideMenuToggler
    >
      <span class="navbar-toggler-icon"></span>
    </button>
  `
})
export class HeaderComponent implements OnInit {
  @HostBinding('class.app-header') class1 = true;
  @HostBinding('class.navbar') class2 = true;

  constructor() { }

  ngOnInit() {
  }

}
