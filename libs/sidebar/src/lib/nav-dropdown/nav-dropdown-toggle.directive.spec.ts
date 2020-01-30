import { NavDropdownToggleDirective } from './nav-dropdown-toggle.directive';
import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { SidebarModule } from '../sidebar.module';
import { RouterTestingModule } from '@angular/router/testing';
import { Component, NgModule, Input, DebugElement } from '@angular/core';
import { NavDropdownDirective } from '.';
import { By } from '@angular/platform-browser';
import { MockRender } from 'ng-mocks';

// * Host component:
@Component({
  selector: 'scx-host-component',
  template: `
    <scx-test-component scxNavDropdownToggle></scx-test-component>
`
})
class HostComponent { }
@Component({
  selector: 'scx-test-component',
  template: ``
})
class TestComponent { }
// @NgModule({
//   declarations: [
//     HostComponent,
//     NavDropdownDirective,
//     NavDropdownToggleDirective
//   ],
//   // imports: [SidebarModule],
//   exports: [
//     HostComponent,
//     NavDropdownDirective,
//     NavDropdownToggleDirective
//   ],
// })
class HostModule {}

xdescribe.only('NavDropdownToggleDirective', () => {
  let component: HostComponent;
  let directiveEl: DebugElement;
  let directiveE2: DebugElement;
  let fixture: ComponentFixture<HostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        HostComponent,
        TestComponent,
        NavDropdownDirective,
        NavDropdownToggleDirective
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = MockRender(`<scx-host-component scxNavDropdown></scx-host-component>`);
    component = fixture.componentInstance;
    directiveEl = fixture.debugElement.query(By.directive(NavDropdownDirective));
    directiveE2 = fixture.debugElement.query(By.directive(NavDropdownToggleDirective));
    fixture.detectChanges();
  });

  it('should create test component', () => {
    expect(component).toBeTruthy();
  });

  it('should create a directive "NavDropdownDirective"', () => {
    expect(directiveEl).toBeTruthy();
  });

  it('should create a directive "NavDropdownToggleDirective"', () => {
    expect(directiveE2).toBeTruthy();
  });

  it('should call toggle on directive instance', () => {
    const directive = directiveEl.injector.get(NavDropdownDirective);
    expect(directive).toBeTruthy();
    spyOn(directive, 'toggle');
    directiveE2.nativeElement.click();
    fixture.whenStable().then(() => {
      expect(directive.toggle).toHaveBeenCalled();
    });
  });

});
