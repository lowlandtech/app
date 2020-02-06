import { InjectionToken, DebugElement, Component } from '@angular/core';
import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { MockRender } from 'ng-mocks';
import { By } from '@angular/platform-browser';
import { MobileSidebarTogglerDirective } from './mobile.sidebar.toggler.directive';
import { AdminModule } from '../../admin.module';
import { StoreModule } from '@ngrx/store';

@Component({
  selector: 'scx-test-component',
  template: ''
})
class TestComponent {}

export const DOCUMENT = new InjectionToken<Document>('DocumentToken');

export function _document(): any {
  return document;
}

describe.only('MobileSidebarTogglerDirective', () => {
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
      `<scx-test-component scxMobileSidebarToggler></scx-test-component>`
    );
    component = fixture.componentInstance;
    directiveEl = fixture.debugElement.query(
      By.directive(MobileSidebarTogglerDirective)
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
    const directive = directiveEl.injector.get(MobileSidebarTogglerDirective);
    expect(directive).toBeTruthy();
  });

});
