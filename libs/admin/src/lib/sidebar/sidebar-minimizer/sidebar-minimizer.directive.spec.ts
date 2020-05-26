import { Component, DebugElement } from '@angular/core';
import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { MockRender } from 'ng-mocks';
import { By } from '@angular/platform-browser';
import { SidebarMinimizerDirective } from '.';
import { AdminModule } from '../../admin.module';

@Component({
  selector: 'scx-test-component',
  template: ''
})
class TestComponent {}

describe('SidebarMinimizerDirective', () => {
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
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = MockRender(
      `<scx-test-component scxSidebarMinimizer></scx-test-component>`
    );
    component = fixture.componentInstance;
    directiveEl = fixture.debugElement.query(
      By.directive(SidebarMinimizerDirective)
    );
    fixture.detectChanges();
  });

  it('should create a host instance', () => {
    expect(component).toBeTruthy();
  });

  it('should create a directive instance', () => {
    expect(directiveEl).toBeTruthy();
  });

  it('should create an instance', () => {
    const directive = directiveEl.injector.get(SidebarMinimizerDirective);
    expect(directive).toBeTruthy();
  });

});
