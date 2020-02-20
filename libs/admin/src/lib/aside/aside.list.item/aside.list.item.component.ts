import { Component, OnInit, ViewChild, ComponentFactoryResolver, Input } from '@angular/core';
import { AsideListGroupDirective } from '../aside.list.group';
import { AsideListGroupModel } from '../models';

@Component({
  selector: 'scx-aside-list-item',
  templateUrl: './aside.list.item.component.html',
  styleUrls: ['./aside.list.item.component.scss']
})
export class AsideListItemComponent implements OnInit {
  @Input() group: AsideListGroupModel;
  @ViewChild(AsideListGroupDirective, {static: true}) host: AsideListGroupDirective;
  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit(): void {
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(this.group.component);
    const viewContainerRef = this.host.viewContainerRef;
    viewContainerRef.clear();
    viewContainerRef.createComponent(componentFactory, 0,this.group.data.injector);
  }
}
