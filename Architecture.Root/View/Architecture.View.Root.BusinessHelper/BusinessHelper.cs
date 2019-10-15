//using Architecture.Common.Logger;
using Architecture.Common.Types;
using Architecture.Types.Root.BusinessHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.View.Root.BusinessHelper
{
    public static partial class BusinessHelper
    {
        public static string TestService(string message)
        {
            var response = Proxy.Executer<CountryRequest, GenericResponse<List<CountryContract>>>.TestService(message);
            return response;

        }
        public static GenericResponse<List<CountryContract>> GetAllCountry()
        {
            CountryRequest request = new CountryRequest();
            request.MethodName = "SelectByColumns";
            var response = Proxy.Executer<CountryRequest, GenericResponse<List<CountryContract>>>.Execute(request);
            if (!response.Success)
            {
                //LogManager.Log("BusinessHelper GetAllCountry " + response.Results.ToString());
            }
            return response;
        }
        public static GenericResponse<List<ParameterContract>> GetParameter(string paramType)
        {
            ParameterRequest request = new ParameterRequest();
            request.MethodName = "SelectByColumns";
            request.ParamType = paramType;
            var response = Proxy.Executer<ParameterRequest, GenericResponse<List<ParameterContract>>>.Execute(request);
            if (!response.Success)
            {
                //LogManager.Log("BusinessHelper GetParameter " + response.Results.ToString());
            }
            return response;
        }
        public static GenericResponse<List<ParameterContract>> GetParameter(string paramType,string paramCode)
        {
            ParameterRequest request = new ParameterRequest();
            request.MethodName = "SelectByColumns";
            request.ParamType = paramType;
            request.ParamCode = paramCode;
            var response = Proxy.Executer<ParameterRequest, GenericResponse<List<ParameterContract>>>.Execute(request);
            if (!response.Success)
            {
                //LogManager.Log("BusinessHelper GetParameter " + response.Results.ToString());
            }
            return response;
        }

        public static Tuple<GenericResponse<List<ResourceContract>>, GenericResponse<List<ActionContract>>> GetResourceAndActionList()
        {
            MultipleRequest mRequest = new MultipleRequest();
            mRequest.RequestList = new List<RequestBase>();

            ResourceRequest request = new ResourceRequest();
            request.MethodName = "SelectByColumns";
            mRequest.RequestList.Add(request);

            ResourceRequest requestAction = new ResourceRequest();
            requestAction.MethodName = "SelectAction";
            mRequest.RequestList.Add(requestAction);

            var mResponse = Proxy.Executer<MultipleRequest, MultipleResponse>.Execute(mRequest);
            if (!mResponse.Success)
            {
                //LogManager.Log("BusinessHelper GetParameter " + mResponse.Results.ToString());
            }
            Tuple<GenericResponse<List<ResourceContract>>, GenericResponse<List<ActionContract>>> returnObject = new Tuple<GenericResponse<List<ResourceContract>>, GenericResponse<List<ActionContract>>>((GenericResponse<List<ResourceContract>>)mResponse.ResponseList[0], (GenericResponse<List<ActionContract>>)mResponse.ResponseList[0]);
            return returnObject;

        }
        public static GenericResponse<List<ActionContract>> GetAction()
        {
            ResourceRequest requestAction = new ResourceRequest();
            requestAction.MethodName = "SelectAction";
            var responseAction = Proxy.Executer<ResourceRequest, GenericResponse<List<ActionContract>>>.Execute(requestAction);
            if (!responseAction.Success)
            {
                //LogManager.Log("BusinessHelper GetParameter " + responseAction.Results.ToString());
            }
            return responseAction;
        }

        public static GenericResponse<List<ResourceContract>> GetResource(string userName,string password, int? resourceId, string resourceCode)
        {
            ResourceRequest request = new ResourceRequest();
            request.MethodName = "SelectByColumns";
            request.UserName = userName;
            request.Password = password;
            request.ResourceId = resourceId;
            request.ResourceCode = resourceCode;
            var response = Proxy.Executer<ResourceRequest, GenericResponse<List<ResourceContract>>>.Execute(request);
            if (!response.Success)
            {
                //LogManager.Log("BusinessHelper GetParameter " + responseAction.Results.ToString());
            }
            return response;
        }
        
        public static Dictionary<string,List<ResourceTreeNode>> GetAllGroupTreeNodeList(Dictionary<string,List<ResourceContract>> dictionaryGroupResource)
        {
            Dictionary<string, List<ResourceTreeNode>> resultList = new Dictionary<string, List<ResourceTreeNode>>();
            foreach (var item in dictionaryGroupResource)
            {
                resultList.Add(item.Key,GetTreeNodeList(item.Value));
            }
            return resultList;
        }

        /// <summary>
        /// Verilen resource listesini ağaç şekline dönüştürür.
        /// </summary>
        /// <param name="resourceList"></param>
        /// <returns></returns>
        public static List<ResourceTreeNode> GetTreeNodeList(List<ResourceContract> resourceList)
        {
            List<ResourceTreeNode> treeNodeList = new List<ResourceTreeNode>();
            ResourceTreeNode treeNew;

            foreach (var item in resourceList.Where(u => u.ParentId == -1).ToList())
            {
                treeNew = new ResourceTreeNode();
                treeNew.Id = item.ResourceId;
                treeNew.Code = item.Code;
                treeNew.Text = item.Text;
                treeNew.Icon = item.Icon;
                treeNew.ParentId = (int)item.ParentId;
                treeNew.IsLeaf = (item.MenuType == (short)Enums.MenuType.MenuLeaf || item.MenuType == (short)Enums.MenuType.SystemLeaf);
                treeNew.ChildList = FindChildsRecursive(treeNew, resourceList);
                treeNodeList.Add(treeNew);
            }
            return treeNodeList;
        }

        /// <summary>
        /// Oluşan ağaç şeklini liste haline getirir.
        /// </summary>
        /// <param name="treeNodeList"></param>
        /// <returns></returns>
        public static List<ResourceTreeNode> TreeToList(List<ResourceTreeNode> treeNodeList)
        {
            List<ResourceTreeNode> CurrentNodeMatches = new List<ResourceTreeNode>();
            foreach (var item in treeNodeList)
            {
                PushStack(item, CurrentNodeMatches);
            }
            return CurrentNodeMatches;
        }

        private static List<ResourceTreeNode> FindChildsRecursive(ResourceTreeNode node, List<ResourceContract> resourceList)
        {
            List<ResourceTreeNode> childList = new List<ResourceTreeNode>();
            var list = resourceList.Where(u => u.ParentId == node.Id && (u.MenuType == (short)Enums.MenuType.MenuLeaf || u.MenuType == (short)Enums.MenuType.MenuParent)).ToList();
            ResourceTreeNode treeNew;
            foreach (var item in list)
            {
                treeNew = new ResourceTreeNode();
                treeNew.Id = item.ResourceId;
                treeNew.Code = item.Code;
                treeNew.Text = item.Text;
                treeNew.Icon = item.Icon;
                treeNew.ParentId = (int)item.ParentId;
                treeNew.IsLeaf = (item.MenuType == (short)Enums.MenuType.MenuLeaf || item.MenuType == (short)Enums.MenuType.SystemLeaf);
                treeNew.ChildList = FindChildsRecursive(treeNew, resourceList);
                childList.Add(treeNew);
            }
            return childList;
        }

        private static void PushStack(ResourceTreeNode node, List<ResourceTreeNode> currentNodeMatches)
        {
            currentNodeMatches.Add(node);
            foreach (var item in node.ChildList)
            {
                PushStack(item, currentNodeMatches);
            }
        }
        
    }
}
