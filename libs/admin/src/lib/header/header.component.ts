import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-header',
  template: `
    <button class="navbar-toggler" type="button" scxMobileSidebarToggler></button>
    <a class="navbar-brand" href="#">
      <span class=" raised-button small-logo text-primary">CS</span>
      <span class="logo">Spotacard</span>
    </a>
    <button class="navbar-toggler" type="button" scxSidebarToggler></button>
    <ng-content></ng-content>
    <button class="navbar-toggler" type="button" scxAsideToggler></button>
  `
})
export class HeaderComponent implements OnInit {
  @HostBinding('class.app-header') class1 = true;
  @HostBinding('class.navbar') class2 = true;

  constructor() {}

  ngOnInit() {

  }
}
