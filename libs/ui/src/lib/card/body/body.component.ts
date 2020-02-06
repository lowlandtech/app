import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-ui-card-body',
  template: `
    <p>
      body works!
    </p>
  `,
  styles: []
})
export class BodyComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
