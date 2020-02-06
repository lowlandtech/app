import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-playground',
  template: `
    <div class="col col-md-12">
      <div class="card">
        <h5 class="card-header">Featured</h5>
        <div class="card-body">
          <h5 class="card-title">Special title treatment</h5>
          <p class="card-text">
            With supporting text below as a natural lead-in to additional
            content.
          </p>
          <a href="#" class="btn btn-primary">Go somewhere</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .col {
      padding-top: 10px;
    }`
  ]
})
export class PlaygroundComponent implements OnInit {
  @HostBinding('class.row') class1 = true;
  constructor() {}

  ngOnInit(): void {}
}
