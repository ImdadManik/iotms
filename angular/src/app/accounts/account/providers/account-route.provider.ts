import { APP_INITIALIZER, inject } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { ACCOUNT_BASE_ROUTES } from './account-base.routes';

export const ACCOUNTS_ACCOUNT_ROUTE_PROVIDER = [
  {
    provide: APP_INITIALIZER,
    multi: true,
    useFactory: configureRoutes,
  },
];

function configureRoutes() {
  const routesService = inject(RoutesService);

  return () => {
    const routes: ABP.Route[] = [...ACCOUNT_BASE_ROUTES];
    routesService.add(routes);
  };
}
