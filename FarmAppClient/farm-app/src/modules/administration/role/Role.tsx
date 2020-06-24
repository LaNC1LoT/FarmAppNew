import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, {
  Editing,
  SearchPanel,
  Column,
  RequiredRule,
  Selection,
  Sorting,
  Scrolling, HeaderFilter, Paging, Pager, FilterRow,
} from 'devextreme-react/tree-list';
import { roles } from "../../../api/mock/roles"
import AspNetData from 'devextreme-aspnet-data-nojquery';
import { BASE_URL } from '../../../core/constants';
import { connect } from 'react-redux';
import { IAppState } from '../../../core/mainReducer';

 const Role = ({user}:{ user: any }) => {
   const allowedPageSizes = [20, 50, 100];

  const rolesData:any = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Roles?page=1&pageSize=2000`,
    insertUrl: `${BASE_URL}api/Roles`,
    updateUrl: `${BASE_URL}api/Roles`,
    deleteUrl: `${BASE_URL}api/Roles`,
    onBeforeSend: function (method, ajaxOptions) {
      ajaxOptions.xhrFields = {withCredentials: false};
    }
  });

   const onCellPrepared = (e: any) => {
     if (e.column.command === 'edit') {
       let addLink = e.cellElement.querySelector('.dx-link-add');

       if (addLink) {
         addLink.remove();
       }
     }
   }


   return (
        <Typography>
            <TreeList
                id="roles"
                dataSource={rolesData}
                onCellPrepared={onCellPrepared}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                keyExpr="id"
            >
              <Scrolling mode="standard" />

              <SearchPanel visible={true}/>
              <HeaderFilter visible={true}/>
              {user?.role?.id === 1 && <Editing
                allowUpdating={true}
                allowDeleting={true}
                allowAdding={true}
                mode="row"
              />
              }

              <Paging
                enabled={true}
                defaultPageSize={20}
              />
              <Pager
                showPageSizeSelector={true}
                allowedPageSizes={allowedPageSizes}
                showInfo={true}
              />
              <FilterRow visible={true}/>
              <Sorting mode="multiple"/>
              <Selection mode="single"/>

                <Column
                    caption={"Номер"}
                    dataType={"number"}
                    visible={false}
                    dataField={"id"}>
                </Column>
                <Column
                    caption={"Имя роли"}
                    dataType={"string"}
                    dataField={"roleName"}>
                    <RequiredRule />
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
  const {auth} = state;
  return {
    user: auth.user
  }
})(Role)
