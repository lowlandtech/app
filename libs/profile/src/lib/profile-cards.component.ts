import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-profile-cards',
  templateUrl: './profile-cards.component.html',
  styleUrls: ['./profile-cards.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileCardsComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
