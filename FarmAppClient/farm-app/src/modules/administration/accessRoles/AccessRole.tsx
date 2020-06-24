import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, {
  Editing,
  SearchPanel,
  Column,
  RequiredRule,
  Selection,
  Sorting,
  Scrolling, Paging, Pager, FilterRow, Lookup,
} from 'devextreme-react/tree-list';
import { apimethodroles } from "../../../api/mock/apimethodroles"
import AspNetData from 'devextreme-aspnet-data-nojquery';
import { BASE_URL } from '../../../core/constants';
import { connect } from 'react-redux';
import { IAppState } from '../../../core/mainReducer';

const AccessRole = ({ user }: { user: any }) => {

  const allowedPageSizes = [20, 50, 100];
  const apiMethodRolesData: any = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/ApiMethodRoles?page=1&pageSize=2000`,
    insertUrl: `${BASE_URL}api/ApiMethodRoles`,
    updateUrl: `${BASE_URL}api/ApiMethodRoles`,
    deleteUrl: `${BASE_URL}api/ApiMethodRoles`,
    onBeforeSend: function(method, ajaxOptions) {
      ajaxOptions.xhrFields = { withCredentials: false };
    },
  });

  const methodsData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/ApiMethods?page=1&pageSize=2000`
  });

  const rolesData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Roles?page=1&pageSize=2000`
  });


  const onCellPrepared = (e: any) => {
        if (e.column.command === 'edit') {
            let addLink = e.cellElement.querySelector('.dx-link-add');

            if (addLink) {
                addLink.remove();
            }
        }
    }
    return(
        <Typography>
        <TreeList
            id="apimethodroles"
            dataSource={apiMethodRolesData}
            showRowLines={true}
            showBorders={true}
            columnAutoWidth={true}
            keyExpr="id"
            onCellPrepared={onCellPrepared}
        >
          <Scrolling mode="standard"/>
          <Paging
            enabled={true}
            defaultPageSize={5}/>
          <Pager
            showPageSizeSelector={true}
            allowedPageSizes={allowedPageSizes}
            showInfo={true}/>
          <FilterRow visible={true}/>
          <Sorting mode="multiple"/>
          <Selection mode="single"/>
          <SearchPanel visible={true}/>
          {user?.role?.id === 1 && <Editing
            allowUpdating={true}
            allowDeleting={true}
            allowAdding={true}
            mode="row"
          />
          }
            <Column
                caption={"Номер"}
                dataType={"number"}
                visible={false}
                dataField={"id"}>
            </Column>
            <Column
                caption={"Метод"}
                dataType={"string"}
                dataField={"apiMethodId"}>
              <RequiredRule />
              <Lookup dataSource={methodsData} valueExpr="id" displayExpr="apiMethodName" />
            </Column>
            <Column
                caption={"Роль"}
                dataType={"string"}
                dataField={"roleId"}>
                <RequiredRule />
              <Lookup dataSource={rolesData} valueExpr="id" displayExpr="roleName" />
            </Column>
            {/*<Column*/}
            {/*    caption={"Удалена"}*/}
            {/*    dataType="boolean"*/}
            {/*    dataField={"isDeleted"}>*/}
            {/*</Column>*/}
        </TreeList>
    </Typography>
    )
}

export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user,
  };
})(AccessRole);
