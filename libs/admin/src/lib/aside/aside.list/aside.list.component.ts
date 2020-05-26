import { Component, OnInit } from '@angular/core';
import { AdminStateFacade } from '../../+state/facades/admin.facade';
import { Observable } from 'rxjs';
import { AsideListGroupModel } from '../models';


@Component({
  selector: 'scx-aside-list',
  templateUrl: './aside.list.component.html',
  styleUrls: ['./aside.list.component.scss']
})
export class AsideListComponent implements OnInit {
  public groups$: Observable<AsideListGroupModel[]>;

  constructor(private facade: AdminStateFacade, ) { }

  ngOnInit(): void {
    this.groups$ = this.facade.groups$;
  }
}
