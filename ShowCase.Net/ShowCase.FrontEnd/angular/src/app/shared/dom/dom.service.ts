import {
  Injectable,
  Injector,
  ComponentFactoryResolver,
  EmbeddedViewRef,
  ApplicationRef
} from '@angular/core';
import {
  CreateCSharpBotInstanceComponent
} from 'src/app/components/csharp-bot/create-csharp-bot-instance/create-csharp-bot-instance.component';

@Injectable()
export class DomService {

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) { }

  showNewInstanceForm(parentId: string,
    component: any,
    botId: number,
    exchangeApis?: any[],
    instruments?: any[]) {

    // 1. Create a component reference from the component
    const componentRef = this.componentFactoryResolver
      .resolveComponentFactory(component)
      .create(this.injector);

    (<CreateCSharpBotInstanceComponent>componentRef.instance).botId = botId;
    (<CreateCSharpBotInstanceComponent>componentRef.instance).exchangeApis = exchangeApis;
    (<CreateCSharpBotInstanceComponent>componentRef.instance).instruments = instruments;

    // 2. Attach component to the appRef so that it's inside the ng component tree
    this.appRef.attachView(componentRef.hostView);

    // 3. Get DOM element from component
    const domElem = (componentRef.hostView as EmbeddedViewRef<any>)
      .rootNodes[0] as HTMLElement;

    // 4. Append DOM element to the body
    const parentEl = document.getElementById(parentId);
    if (parentEl) {
      if (parentEl.hasChildNodes) {
        parentEl.insertBefore(domElem, parentEl.firstChild);
      } else {
        parentEl.appendChild(domElem);
      }
    }
  }

  addNewInstanceCard(parentId: string,
    component: any, instanceModel: any, exchangeApis?: any[], instruments?: any[]) {
    // 1. Create a component reference from the component
    const componentRef = this.componentFactoryResolver
      .resolveComponentFactory(component)
      .create(this.injector);

    (<CreateCSharpBotInstanceComponent>componentRef.instance).instanceModel = instanceModel;
    (<CreateCSharpBotInstanceComponent>componentRef.instance).exchangeApis = exchangeApis;
    (<CreateCSharpBotInstanceComponent>componentRef.instance).instruments = instruments;

    // 2. Attach component to the appRef so that it's inside the ng component tree
    this.appRef.attachView(componentRef.hostView);

    // 3. Get DOM element from component
    const domElem = (componentRef.hostView as EmbeddedViewRef<any>)
      .rootNodes[0] as HTMLElement;

    // 4. Append DOM element to the body
    const parentEl = document.getElementById(parentId);
    if (parentEl) {
      parentEl.appendChild(domElem);
    }
  }

  removeComponent(htmlId: string, component: any) {
    if (component) {
      const componentRef = this.componentFactoryResolver
        .resolveComponentFactory(component);

      const cmp = this.appRef.components.find(c => c.componentType === componentRef.componentType);
      if (cmp) {
        cmp.destroy();
      }
    }

    if (htmlId) {
      const componentEl = document.getElementById(htmlId);
      if (componentEl) {
        componentEl.remove();
      }
    }
  }
}
