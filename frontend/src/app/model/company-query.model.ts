import { Company } from "./company.model";
import { PageInfo } from "./page-info.model";

export interface CompanyQuery {
  pageInfo: PageInfo;
  items: Company[];
}
