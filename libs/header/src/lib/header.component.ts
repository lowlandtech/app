import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-header',
  template: `
    <button class="navbar-toggler d-lg-none" type="button" scxMobileSidebarToggler>
      <span class="navbar-toggler-icon"></span>
    </button>
    <a class="navbar-brand" href="#"></a>
    <button class="navbar-toggler d-md-down-none mr-auto" type="button" scxSidebarToggler>
      <span class="navbar-toggler-icon"></span>
    </button>
    <button class="navbar-toggler d-md-down-none" type="button" scxAsideMenuToggler>
      <span class="navbar-toggler-icon"></span>
    </button>
  `,
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  @HostBinding('class.app-header') class1 = true;
  @HostBinding('class.navbar') class2 = true;

  constructor() { }

  ngOnInit() {
  }

}
