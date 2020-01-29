import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarNavLinkComponent } from './sidebar-nav-link.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('SidebarNavLinkComponent', () => {
  let component: SidebarNavLinkComponent;
  let fixture: ComponentFixture<SidebarNavLinkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SidebarNavLinkComponent ],
      imports: [ RouterTestingModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarNavLinkComponent);
    component = fixture.componentInstance;
    component.link = {
      url: ''
    };
    fixture.detectChanges();
  });

it('should create', () => {
    expect(component).toBeTruthy();
  });

});
