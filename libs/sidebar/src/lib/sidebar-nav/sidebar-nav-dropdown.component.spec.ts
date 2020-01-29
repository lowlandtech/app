import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SidebarNavDropdownComponent } from './sidebar-nav-dropdown.component';
import { SidebarModule } from '../sidebar.module';

describe('SidebarNavDropdownComponent', () => {
  let component: SidebarNavDropdownComponent;
  let fixture: ComponentFixture<SidebarNavDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [SidebarModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarNavDropdownComponent);
    component = fixture.componentInstance;
    component.link = {icon: ''};
    fixture.detectChanges();
  });

it('should create', () => {
    expect(component).toBeTruthy();
  });

});
