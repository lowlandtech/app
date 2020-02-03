import { Component, OnInit, ChangeDetectionStrategy, HostBinding } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AsideFacade } from '@spotacard/aside';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'body',
  template: `
    <router-outlet></router-outlet>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit {
  @HostBinding('aside-menu-hidden') asideMenuHidden = true;

  constructor(
    private router: Router,
    private aside: AsideFacade) { }

  ngOnInit() {
    this.router.events.subscribe(evt => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
    this.aside.toggled$.subscribe(()=>{
      this.asideMenuHidden = !this.asideMenuHidden;
    });
  }
}
