import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-main',
  template: `
    <ng-content>
    </ng-content>
  `,
  styles: []
})
export class MainComponent implements OnInit {
  @HostBinding('class.main') class1 = true;

  constructor() { }

  ngOnInit() {
  }

}
