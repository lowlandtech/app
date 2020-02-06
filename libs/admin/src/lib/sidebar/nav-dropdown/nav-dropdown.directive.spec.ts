import { NavDropdownDirective } from './nav-dropdown.directive';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { Component, DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';
import { MockRender } from 'ng-mocks';
import { AdminModule } from '../../admin.module';
import { StoreModule } from '@ngrx/store';

@Component({
  selector: 'scx-test-component',
  template: ''
})
class TestComponent { }

describe('NavDropdownDirective', () => {
  let component: TestComponent;
  let directiveEl: DebugElement;
  let fixture: ComponentFixture<TestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdminModule
      ],
      declarations: [
        TestComponent
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = MockRender(`<scx-test-component scxNavDropdown></scx-test-component>`);
    component = fixture.componentInstance;
    directiveEl = fixture.debugElement.query(By.directive(NavDropdownDirective));
    fixture.detectChanges();
  });

  it('should create a host instance', () => {
    expect(component).toBeTruthy();
  });

  it('should create a directive instance', () => {
    expect(directiveEl).toBeTruthy();
  });

  it('should create an instance', () => {
    const directive = directiveEl.injector.get(NavDropdownDirective);
    expect(directive).toBeTruthy();
  });

});
