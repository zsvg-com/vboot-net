var A=Object.defineProperty;var B=Object.getOwnPropertySymbols;var U=Object.prototype.hasOwnProperty,R=Object.prototype.propertyIsEnumerable;var C=(e,u,a)=>u in e?A(e,u,{enumerable:!0,configurable:!0,writable:!0,value:a}):e[u]=a,V=(e,u)=>{for(var a in u||(u={}))U.call(u,a)&&C(e,a,u[a]);if(B)for(var a of B(u))R.call(u,a)&&C(e,a,u[a]);return e};import{_ as M}from"./index.1652784183534.js";import{a as $,a3 as z,ag as L,b as g,Z as l,S as o,a2 as n,o as r,e as h,F as P,a7 as I,R as _,X as D}from"./vue.1652784183534.js";const O=$({name:"systemEditDic",setup(){const e=z({isShowDialog:!1,ruleForm:{dicName:"",fieldName:"",status:!0,list:[{id:Math.random(),label:"",value:""}],describe:"",fieldNameList:[]}}),u=s=>{s.fieldName==="SYS_UERINFO"?s.list=[{id:Math.random(),label:"sex",value:"1"},{id:Math.random(),label:"sex",value:"0"}]:s.list=[{id:Math.random(),label:"role",value:"admin"},{id:Math.random(),label:"role",value:"common"},{id:Math.random(),label:"roleName",value:"\u8D85\u7EA7\u7BA1\u7406\u5458"},{id:Math.random(),label:"roleName",value:"\u666E\u901A\u7528\u6237"}],e.ruleForm=s,e.isShowDialog=!0},a=()=>{e.isShowDialog=!1};return V({openDialog:u,closeDialog:a,onCancel:()=>{a()},onSubmit:()=>{a()},onAddRow:()=>{e.ruleForm.list.push({id:Math.random(),label:"",value:""})},onDelRow:s=>{e.ruleForm.list.splice(s,1)}},L(e))}}),T={class:"system-edit-dic-container"},X=h("span",{class:"ml10"},"\u5B57\u6BB5",-1),Y={class:"dialog-footer"},Z=D("\u53D6 \u6D88"),j=D("\u4FEE \u6539");function q(e,u,a,w,E,v){const f=n("el-alert"),s=n("el-input"),m=n("el-form-item"),d=n("el-col"),y=n("el-switch"),N=n("ele-Plus"),F=n("el-icon"),c=n("el-button"),x=n("ele-Delete"),b=n("el-row"),S=n("el-form"),k=n("el-dialog");return r(),g("div",T,[l(k,{title:"\u4FEE\u6539\u5B57\u5178",modelValue:e.isShowDialog,"onUpdate:modelValue":u[4]||(u[4]=t=>e.isShowDialog=t),width:"769px"},{footer:o(()=>[h("span",Y,[l(c,{onClick:e.onCancel,size:"default"},{default:o(()=>[Z]),_:1},8,["onClick"]),l(c,{type:"primary",onClick:e.onSubmit,size:"default"},{default:o(()=>[j]),_:1},8,["onClick"])])]),default:o(()=>[l(f,{title:"\u534A\u6210\u54C1\uFF0C\u4EA4\u4E92\u8FC7\u4E8E\u590D\u6742\uFF0C\u8BF7\u81EA\u884C\u6269\u5C55\uFF01",type:"warning",closable:!1,class:"mb20"}),l(S,{model:e.ruleForm,size:"default","label-width":"90px"},{default:o(()=>[l(b,{gutter:35},{default:o(()=>[l(d,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:o(()=>[l(m,{label:"\u5B57\u5178\u540D\u79F0"},{default:o(()=>[l(s,{modelValue:e.ruleForm.dicName,"onUpdate:modelValue":u[0]||(u[0]=t=>e.ruleForm.dicName=t),placeholder:"\u8BF7\u8F93\u5165\u5B57\u5178\u540D\u79F0",clearable:""},null,8,["modelValue"])]),_:1})]),_:1}),l(d,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:o(()=>[l(m,{label:"\u5B57\u6BB5\u540D"},{default:o(()=>[l(s,{modelValue:e.ruleForm.fieldName,"onUpdate:modelValue":u[1]||(u[1]=t=>e.ruleForm.fieldName=t),placeholder:"\u8BF7\u8F93\u5165\u5B57\u6BB5\u540D\uFF0C\u62FC\u63A5 ruleForm.list",clearable:""},null,8,["modelValue"])]),_:1})]),_:1}),l(d,{xs:24,sm:24,md:24,lg:24,xl:24,class:"mb20"},{default:o(()=>[l(m,{label:"\u5B57\u5178\u72B6\u6001"},{default:o(()=>[l(y,{modelValue:e.ruleForm.status,"onUpdate:modelValue":u[2]||(u[2]=t=>e.ruleForm.status=t),"inline-prompt":"","active-text":"\u542F","inactive-text":"\u7981"},null,8,["modelValue"])]),_:1})]),_:1}),l(d,{xs:24,sm:24,md:24,lg:24,xl:24,class:"mb20"},{default:o(()=>[(r(!0),g(P,null,I(e.ruleForm.list,(t,i)=>(r(),_(b,{gutter:35,key:i},{default:o(()=>[l(d,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:o(()=>[l(m,{prop:`list[${i}].label`},{label:o(()=>[i===0?(r(),_(c,{key:0,type:"primary",circle:"",size:"small",onClick:e.onAddRow},{default:o(()=>[l(F,null,{default:o(()=>[l(N)]),_:1})]),_:1},8,["onClick"])):(r(),_(c,{key:1,type:"danger",circle:"",size:"small",onClick:p=>e.onDelRow(i)},{default:o(()=>[l(F,null,{default:o(()=>[l(x)]),_:1})]),_:2},1032,["onClick"])),X]),default:o(()=>[l(s,{modelValue:t.label,"onUpdate:modelValue":p=>t.label=p,style:{width:"100%"},placeholder:"\u8BF7\u8F93\u5165\u5B57\u6BB5\u540D"},null,8,["modelValue","onUpdate:modelValue"])]),_:2},1032,["prop"])]),_:2},1024),l(d,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:o(()=>[l(m,{label:"\u5C5E\u6027",prop:`list[${i}].value`},{default:o(()=>[l(s,{modelValue:t.value,"onUpdate:modelValue":p=>t.value=p,style:{width:"100%"},placeholder:"\u8BF7\u8F93\u5165\u5C5E\u6027\u503C"},null,8,["modelValue","onUpdate:modelValue"])]),_:2},1032,["prop"])]),_:2},1024)]),_:2},1024))),128))]),_:1}),l(d,{xs:24,sm:24,md:24,lg:24,xl:24,class:"mb20"},{default:o(()=>[l(m,{label:"\u5B57\u5178\u63CF\u8FF0"},{default:o(()=>[l(s,{modelValue:e.ruleForm.describe,"onUpdate:modelValue":u[3]||(u[3]=t=>e.ruleForm.describe=t),type:"textarea",placeholder:"\u8BF7\u8F93\u5165\u5B57\u5178\u63CF\u8FF0",maxlength:"150"},null,8,["modelValue"])]),_:1})]),_:1})]),_:1})]),_:1},8,["model"])]),_:1},8,["modelValue"])])}var K=M(O,[["render",q]]);export{K as default};
